class BookModel {
  constructor(data) {
    this.bookId = data.bookId;
    this.title = data.title;
    this.isbn = data.isbn;
    this.publishedDate = data.publishedDate;
    this.thumbnailUrl = data.thumbnailUrl;
    this.descr = data.descr || data.shortDescr;
    this.longDescr = data.longDescr;
  }
}

export default BookModel;
