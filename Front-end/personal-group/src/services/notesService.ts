import type { NotesRequest } from '~/types/request/notes-request'
import type { NotesResponse } from '../types/response/notes-response'


//#region  GET
export const getNotes = async (): Promise<NotesResponse[]> => {
    const config = useRuntimeConfig()
    const API_URL = config.public.apiBaseUrl + "api/Notes"

    const res = await fetch(API_URL)
    return res.json()
}
//#endregion


//#region POST
export const createNote = async (note: NotesRequest) => {
    const config = useRuntimeConfig()
    const API_URL = config.public.apiBaseUrl + "api/Notes"

    const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ content: note })
    })
    return res.json()
}
//#endregion

//#region PUT

//#endregion