<template>
    <div v-if="book">
        <b-navbar toggleable="md" type="dark" variant="info">
            <b-navbar-nav>
                <b-nav-item :to="{ name: 'book_edit', params: { id: book.bookId } }">Edit</b-nav-item>
            </b-navbar-nav>
        </b-navbar>
        <b-card :title="book.title"
                :img-src="book.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem"
                class="mb-2">
            <b-card-text>
                {{ book.descr }}
            </b-card-text>
        </b-card>
    </div>
</template>

<script>
    import axios from "axios";
    import settings from "@/settings";

    export default {
        name: "Book",
        props: ["id"],
        data: () => ({
            book: null,
        }),
        mounted() {
            axios
                .get(`${settings.api.base}${settings.api.books}${this.id}`)
                .then((response) => {
                    this.book = response.data;
                });
        },
    };
</script>
