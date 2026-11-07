import 'package:flutter/material.dart';
import '../../../data/connection.dart';

class BookDetailsScreen extends StatelessWidget {
  final dynamic book;

  const BookDetailsScreen(this.book, {super.key});

  @override
  Widget build(BuildContext context) {
    final title = book['title'] ?? book['Title'] ?? 'N/A';
    final author = book['author'] ?? book['Author'] ?? 'N/A';
    final price = book['price'] ?? book['Price'] ?? 0.00;

    final String imagePath = book['imageUrl']?.toString() ?? book['ImageUrl']?.toString() ?? '';
    final String imageUrl = imagePath.isEmpty
        ? ''
        : Uri.parse(ConnectionString.dashboard)
        .resolve(imagePath)
        .toString();

    return Scaffold(
      appBar: AppBar(
        title: const Text('Book Details'),
        backgroundColor: Colors.indigo,
      ),
      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            SizedBox(
              width: double.infinity,
              height: 300,
              child: imageUrl.isNotEmpty
                  ? Image.network(
                imageUrl,
                fit: BoxFit.contain,
                errorBuilder: (context, error, stackTrace) {
                  return const Icon(Icons.book, size: 100, color: Colors.grey);
                },
              )
                  : const Icon(Icons.book, size: 100, color: Colors.grey),
            ),
            const SizedBox(height: 20),
            Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    title.toString(),
                    style: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 10),
                  Text(
                    'Author: $author',
                    style: const TextStyle(
                      fontSize: 18,
                      color: Colors.indigo,
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                  const SizedBox(height: 10),
                  Text(
                    'Price: \$$price',
                    style: const TextStyle(
                      fontSize: 20,
                      color: Colors.green,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}