import { ref } from 'vue'
import { getNotes } from '@/services/notesService'
import type { NotesResponse } from '~/types/response/notes-response'
const savedNotes = ref(<NotesResponse[]>[])

export const useNotes = () => {

    const fetchNotes = async () => {
        try {
            const notes = await getNotes()
            console.log('Notas recebidas da API:', notes) // Aqui o log das notas recebidas da API
            savedNotes.value = notes
        } catch (error) {
            console.error('Erro ao buscar notas:', error)
        }
    }

    function addNote(note: any) {
        savedNotes.value.unshift(note) // coloca no topo da lista
        console.log('Nota adicionada:', savedNotes) // Aqui o log da nota adicionada
    }

    //   const saveNote = async (text) => {
    //     const newNote = await createNote(text)
    //     savedNotes.value.unshift(newNote) // aparece no topo
    //   }

    return { savedNotes, fetchNotes, addNote }
}
