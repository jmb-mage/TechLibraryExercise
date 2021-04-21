<template>
    <div id="app">
        <b-container>
            <b-navbar variant="lg">
                <b-navbar-nav>
                    <b-nav-item href="/">Home</b-nav-item>
                    <b-nav-item v-if="this.$route.name != 'book_edit'" :to="{ name: 'book_add' }">Add</b-nav-item>
                </b-navbar-nav>
            </b-navbar>
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
