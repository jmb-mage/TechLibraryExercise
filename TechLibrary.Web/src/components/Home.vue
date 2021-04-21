<template>
    <div class="home">
        <h1>{{ msg }}</h1>

        <b-navbar toggleable="md" type="dark" variant="info">
            <b-navbar-nav>
                <b-pagination v-model="page"
                              :total-rows="count"
                              :per-page="perPage"
                              aria-controls="books-table"
                              align="center"></b-pagination>
            </b-navbar-nav>
            <b-navbar-nav class="ml-auto">
                <b-nav-form>
                    <b-form-input v-model="query"
                                  @keyup="onSearch"
                                  placeholder="Search"></b-form-input>
                </b-nav-form>
            </b-navbar-nav>
        </b-navbar>

        <b-table striped
                 hover
                 id="books-table"
                 :per-page="perPage"
                 :current-page="page"
                 :items="dataContext"
                 :fields="fields"
                 responsive="sm">
            <template v-slot:cell(thumbnailUrl)="data">
                <b-img :src="data.value" thumbnail fluid></b-img>
            </template>
            <template v-slot:cell(title_link)="data">
                <b-link :to="{ name: 'book_view', params: { id: data.item.bookId } }">
                    {{ data.item.title }}
                </b-link>
            </template>
        </b-table>
    </div>
</template>

<script>
    import axios from "axios";
    import settings from "@/settings";

    export default {
        name: "Home",
        props: {
            msg: String,
        },
        data: () => ({
            fields: [
                { key: "thumbnailUrl", label: "Book Image" },
                {
                    key: "title_link",
                    label: "Book Title",
                    sortable: true,
                    sortDirection: "desc",
                },
                { key: "isbn", label: "ISBN", sortable: true, sortDirection: "desc" },
                {
                    key: "descr",
                    label: "Description",
                    sortable: true,
                    sortDirection: "desc",
                },
            ],
            items: [],
            page: 1,
            perPage: 10,
            count: 1,
            query: "",
        }),

        methods: {
            dataContext(ctx, callback) {
                axios
                    .get(
                        `${settings.api.base}${settings.api.page}${this.page - 1}/${this.perPage
                        }/${this.query}`
                    )
                    .then((response) => {
                        this.count = response.data.count;
                        callback(response.data.books);
                    });
            },
            onSearch() {
                this.$root.$emit("bv::refresh::table", "books-table");
            },
        },
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped></style>
