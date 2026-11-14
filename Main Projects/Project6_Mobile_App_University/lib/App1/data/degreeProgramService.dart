import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class DegreeProgramService {

  Future<List<dynamic>> getDegreePrograms() async {

    final response = await http.get(
      Uri.parse(ConnectionString.degreeProgram),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load degree programs');
    }
  }


}