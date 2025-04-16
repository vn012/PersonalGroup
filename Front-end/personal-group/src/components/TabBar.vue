<template>
  <div class="tab-bar fixed bottom-26 left-1/2 transform -translate-x-1/2 bg-gray-800 shadow-2xl rounded-2xl px-10 py-3 flex gap-10 items-center z-50">
    <NuxtLink
      v-for="tab in tabs"
      :key="tab.name"
      :to="tab.route"
      class="tab flex flex-col items-center text-gray-300 hover:text-white transition"
      :class="{ 'text-blue-400 font-bold': isActive(tab.route) }"
    >
      <span class="text-sm">{{ tab.label }}</span>
    </NuxtLink>
  </div>
</template>

<script setup>
import { useRoute } from 'vue-router'

const route = useRoute()

const tabs = [
  { name: 'notes', label: 'Notas', route: '/notes-page', icon: 'ph:note-pencil-bold' },
  { name: 'links', label: 'Links', route: '/links-page', icon: 'ph:link-bold' }
]

function isActive(tabRoute) {
  return route.path === tabRoute
}
</script>
<style scoped>
.custom-tab-bar {
  backdrop-filter: blur(10px);
  transition: all 1s ease-in-out;
}
</style>