import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class EnrollmentService {

  Future<List<dynamic>> getEnrollments() async {

    final response = await http.get(
      Uri.parse(ConnectionString.enrollment),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load enrollments');
    }
  }


}