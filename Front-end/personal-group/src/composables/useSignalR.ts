// composables/useSignalR.ts
import * as signalR from '@microsoft/signalr';
import { useNotes } from './useNotes'

let connection: signalR.HubConnection | null = null;

export const useSignalR = () => {
    const startConnection = async (userId: number) => {
        if (connection) return;

        const config = useRuntimeConfig()
        const API_URL = `${config.public.apiBaseUrl}noteHub?userId=${userId}`
        const { addNote } = useNotes()

        connection = new signalR.HubConnectionBuilder()
            .withUrl(API_URL, {
                withCredentials: true,
            })
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveUpdate", (messageObj) => {
            console.log("Nova nota recebida:", messageObj);
            addNote(messageObj)
            // Emitir um event ou usar uma store para atualizar UI
        });

        try {
            await connection.start();
            console.log("✅ Conexão SignalR iniciada");
        } catch (err) {
            console.error("❌ Erro ao conectar com SignalR:", err);
        }
    };

    const stopConnection = async () => {
        if (connection) {
            await connection.stop();
            connection = null;
            console.log("🛑 Conexão SignalR encerrada");
        }
    };

    return { startConnection, stopConnection };
};
