import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class StudentService {

  Future<List<dynamic>> getStudents() async {

    final response = await http.get(
      Uri.parse(ConnectionString.students),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load students');
    }
  }

}
