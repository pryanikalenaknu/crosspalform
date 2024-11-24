<script setup>

import Layout from "@/components/Layout.vue";
</script>

<template>
  <header>
      <layout></layout>
  </header>
  <div class="container">
    <main role="main" class="pb-3">
      <router-view></router-view>
    </main>
  </div>

</template>

<style scoped>

.footer {
  position: absolute;
  bottom: 0;
  width: 100%;
  white-space: nowrap;
  line-height: 60px;
}
.border-top {
  border-top: 1px solid #e5e5e5;
}
</style>
