import 'package:flutter/material.dart';
import '../../App2/data/bookService.dart';

class BookProvider extends ChangeNotifier {
  final BookService bookService = BookService();

  List<dynamic> books = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchBooks() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      books = await bookService.getBooks();
    } catch (e) {
      error = 'Failed to load books: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}