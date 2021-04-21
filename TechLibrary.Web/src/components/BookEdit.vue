<template>
  <div v-if="book">
    <b-form class="mb-2">
      <b-form-group id="input-group-1" label="Title:" label-for="input-1">
        <b-form-input
          id="input-1"
          v-model="book.title"
          placeholder="Enter Title"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        v-if="!isEdit"
        id="input-group-2"
        label="ISBN:"
        label-for="input-2"
      >
        <b-form-input
          id="input-2"
          v-model="book.isbn"
          placeholder="Enter ISBN"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        v-if="!isEdit"
        id="input-group-3"
        label="Published:"
        label-for="input-3"
      >
        <b-form-datepicker
          id="input-3"
          v-model="published"
          class="mb-2"
        ></b-form-datepicker>
      </b-form-group>
      <b-form-group
        id="input-group-4"
        label="Thumbnail url:"
        label-for="input-4"
      >
        <b-form-input
          id="input-4"
          v-model="book.thumbnailUrl"
          placeholder="Enter thumbnail url"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-5" label="Description:" label-for="input-5">
        <b-form-textarea
          id="input-5"
          v-model="book.descr"
          placeholder="Enter a description"
          rows="3"
          max-rows="6"
        ></b-form-textarea>
      </b-form-group>
      <b-form-group
        v-if="!isEdit"
        id="input-group-6"
        label="Long Description:"
        label-for="input-6"
      >
        <b-form-textarea
          id="input-6"
          v-model="book.longDescr"
          placeholder="Enter a long description"
          rows="3"
          max-rows="6"
        ></b-form-textarea>
      </b-form-group>
      <div v-if="msg" class="mb-2">{{ msg }}</div>
      <b-button-group>
        <b-button @click.once="onSubmit" variant="primary" class="mr-2"
          >Submit</b-button
        >
        <b-button
          v-if="isEdit"
          :to="{ name: 'book_view', params: { id: book.bookId } }"
          variant="primary"
          >View Book</b-button
        >
      </b-button-group>
    </b-form>
  </div>
</template>

<script>
import axios from "axios";
import settings from "@/settings";
import BookModel from "@/models/BookModel";

export default {
  name: "BookEdit",
  props: ["id"],
  data: () => ({
    book: null,
    isEdit: false,
    published: null,
    msg: null,
  }),
  mounted() {
    this.isEdit = this.id && this.id > 0;

    if (this.isEdit) {
      axios
        .get(`${settings.api.base}${settings.api.books}${this.id}`)
        .then((response) => {
          this.book = new BookModel(response.data);
        });
    } else {
      this.book = new BookModel({});
    }
  },
  methods: {
    onSubmit() {
      if (this.isEdit) {
        this.book.bookId = Number(this.id);
        this.book.shortDescr = this.book.descr;
        axios
          .post(`${settings.api.base}${settings.api.edit}`, this.book)
          .then((response) => {
            this.book = new BookModel(response.data);
            this.msg = "Book edited.";
          });
      } else {
        this.book.bookId = 0;
        this.book.shortDescr = this.book.descr;
        this.book.publishedDate = this.published;
        axios
          .post(`${settings.api.base}${settings.api.add}`, this.book, {
            maxRedirects: 0,
          })
          .then((response) => {
            this.msg = "Book added.";
            const id = response.data.bookId;
            if (id) this.$router.push(`/book/${id}`);
          })
          .catch((e) => {
            console.log("Added error", e);
          });
      }
    },
  },
};
</script>
