<template>
  <div id="app">
    <b-container>
      <b-button-group>
        <b-button to="/" variant="primary" class="mr-2">Home</b-button>
        <b-button
          v-if="this.$route.name != 'book_edit'"
          :to="{ name: 'book_add' }"
          variant="primary"
          class="mr-2"
          >Add</b-button
        >
      </b-button-group>
      <router-view> </router-view>
    </b-container>
  </div>
</template>

<script>
export default {
  name: "app",
};
</script>

<style></style>
