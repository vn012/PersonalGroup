export interface NotesResponse {
    id: number; 
    userId: number;
    text: string;
    createdAt: string; // ou Date, se você for converter ao receber
    updatedAt?: string; // idem
    deletedAt: string | null;
    tags?: Tag[];
    mediaItems: MediaItem[];
  }
  
  export interface Tag {
    id: number;
    name: string;
  }
  
  export interface MediaItem {
    mediaType: string;
    url: string;    
    thumbnailUrl?: string; 
    metaData?: string; 
    createdAt?: string; 
  }
  