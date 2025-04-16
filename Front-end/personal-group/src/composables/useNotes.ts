import { ref } from 'vue'
import { getNotes } from '@/services/notesService'
import type { NotesResponse } from '~/types/response/notes-response'

export const useNotes = () => {
    const savedNotes = ref(<NotesResponse[]>[])

    const fetchNotes = async () => {
        try {
            const notes = await getNotes()
            console.log('Notas recebidas da API:', notes) // Aqui o log das notas recebidas da API
            savedNotes.value = notes
        } catch (error) {
            console.error('Erro ao buscar notas:', error)
        }
    }

    //   const saveNote = async (text) => {
    //     const newNote = await createNote(text)
    //     savedNotes.value.unshift(newNote) // aparece no topo
    //   }

    return { savedNotes: savedNotes, fetchNotes }
}
