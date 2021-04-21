<template>
  <div v-if="book">
    <b-card
      :title="book.title"
      :img-src="book.thumbnailUrl"
      img-alt="Image"
      img-top
      tag="article"
      style="max-width: 30rem"
      class="mb-2"
    >
      <b-card-text>
        {{ book.descr }}
      </b-card-text>

      <b-button-group>
        <b-button
          :to="{ name: 'book_edit', params: { id: book.bookId } }"
          variant="primary"
          >Edit</b-button
        >
      </b-button-group>
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
