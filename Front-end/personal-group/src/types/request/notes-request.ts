export interface NotesRequest {
    userId: number;
    text: string;
    tags: Tag[];
    mediaItems: MediaItem[];
}

export interface Tag {
    id: number;
    name: string;
}

export interface MediaItem {
    mediaTypeId: number;
    url: string;
    metadata?: string;
}
