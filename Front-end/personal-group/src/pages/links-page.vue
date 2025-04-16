<!-- pages/LinksPage.vue -->
<template>
    <div class="min-h-screen flex flex-col justify-end p-4 pb-0 pb-38">
        <!-- Área de notas com comportamento correto -->
        <div ref="noteList"
            class="flex flex-col-reverse gap-2 w-full max-w-md mx-auto overflow-y-auto grow no-scrollbar"
            :style="{ maxHeight: 'calc(100vh - 180px)', }">
            <div v-for="(note, index) in savedNotes" :key="index" class="text-center rounded-md p-2 shadow">
                {{ note }}
            </div>
        </div>

        <TextInput />

    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import TextInput from '~/components/TextInput.vue'

const newLink = ref('')
const savedLinks = ref([])

function saveLink() {
    if (newLink.value.trim()) {
        savedLinks.value.push(newLink.value)
        newLink.value = ''
    }
}
</script>

<style scoped>
input {
    height: 40px;
}
</style>