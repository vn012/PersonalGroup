<!-- pages/LinksPage.vue -->
<template>
    <div class="min-h-screen flex flex-col justify-end p-4 pb-0 pb-38">
        <!-- Área de notas com comportamento correto -->


        <TextInput />

    </div>
</template>

<script setup lang="ts">

import * as signalR from '@microsoft/signalr';
let isConnected = false;  // Flag para controlar a conexão

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7004/noteHub?userId=1", {
        withCredentials: true,
    })
    .build();

connection.on("ReceiveUpdate", (message) => {
    console.log("Nova nota recebida:", message);

    // atualizar UI
});

onMounted(async () => {
    // Garantir que a conexão só seja estabelecida uma vez
    if (!isConnected) {
        try {
            await connection.start();

            // Marca como conectado
            isConnected = true;
        } catch (err) {
            console.error("Erro ao conectar com o SignalR:", err);
        }
    } else {
        console.log("Conexão já estabelecida.");
    }
});


</script>

<style scoped>
input {
    height: 40px;
}
</style>