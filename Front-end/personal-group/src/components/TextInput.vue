<!-- eslint-disable vue/first-attribute-linebreak -->
<template>
    <div
        class="fixed bottom-0 left-1/2 transform -translate-x-1/2 w-full max-w-xl shadow-lg p-2 rounded-t-xl 0 bg-gray-400 ">
        <div class="flex items-end gap-4">
            <textarea v-model="input" placeholder="Digite..."
                class="p-4 w-full rounded-md resize-none bg-gray-400 text-white border-gray-400 focus:outline-none focus:ring-1 focus:ring-gray-500"
                rows="2" />
            <button class="bg-blue-500 hover:bg-blue-400 text-white p-2 rounded-full flex items-center justify-center"
                @click="emitNote">
                <Icon name="heroicons-outline:paper-airplane" class="w-6 h-6 transform rotate-45" />
            </button>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue'

const emit = defineEmits(['submit'])
const input = ref('')

function emitNote() {
    if (input.value.trim()) {
        emit('submit', input.value)
        input.value = ''
    }
}
</script>