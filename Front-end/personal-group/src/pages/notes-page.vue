<!-- eslint-disable vue/first-attribute-linebreak -->
<template>
  <!-- Container da página -->
  <div class="min-h-screen flex flex-col justify-end items-center p-4 pb-0 pb-40">
    <!-- Área de notas com comportamento correto -->
    <div ref="noteList" class="flex flex-col-reverse gap-3 w-full max-w-xl overflow-y-auto grow px-0 no-scrollbar"
      :style="{ maxHeight: 'calc(100vh - 220px)' }">
      <div v-for="(note, index) in savedNotes" :key="note.id ?? index"
        class="bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg p-3 text-sm text-gray-800 dark:text-gray-200 shadow-sm">
        {{ note.text }}
      </div>
    </div>

    <!-- Input fixo -->
    <TextInput />
  </div>
</template>


<script setup lang="ts">
import { ref, onMounted } from 'vue'
import TextInput from '@/components/TextInput.vue'
import { useNotes } from '#imports'


const noteList = ref<HTMLElement | null>(null)
const { savedNotes, fetchNotes } = useNotes()

onMounted(() => {
  fetchNotes()
  console.log('Notas carregadas:', savedNotes)
})


// function saveNote(noteContent: NotesRequest) {



//   nextTick(() => {
//     noteList.value?.scrollTo({
//       top: 0,
//       behavior: 'smooth'
//     })
//   })
// }


</script>


<style scoped>
.no-scrollbar::-webkit-scrollbar {
  display: none;
}

.no-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
</style>
