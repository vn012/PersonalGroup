<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <div>
    <div class="bg-gray-50 dark:bg-gray-900 text-white">
      <TabBar />
      <NuxtPage />
    </div>

  </div>
</template>

<script setup lang="ts">
import TabBar from '~/components/TabBar.vue'
import { useSignalR } from '~/composables/useSignalR';

onMounted(() => {
  const { startConnection } = useSignalR();
  const userId = 1;
  startConnection(userId);
});

</script>