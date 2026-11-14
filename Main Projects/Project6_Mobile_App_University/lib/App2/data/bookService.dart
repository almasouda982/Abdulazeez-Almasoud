import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class BookService {



  Future<List<dynamic>> getBooks() async {

    final response = await http.get(
      Uri.parse(ConnectionString.books),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load books');
    }
  }





}