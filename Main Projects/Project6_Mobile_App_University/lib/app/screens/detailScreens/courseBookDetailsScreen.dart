import 'package:flutter/material.dart';
import '../../../data/connection.dart';

class CourseBookDetailsScreen extends StatelessWidget {
  final dynamic book;

  const CourseBookDetailsScreen(this.book, {super.key});

  @override
  Widget build(BuildContext context) {
    final String imagePath = book['imageUrl']?.toString() ?? '';
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
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Center(
                child: SizedBox(
                  height: 250,
                  child: imageUrl.isNotEmpty
                      ? Image.network(
                    imageUrl,
                    fit: BoxFit.contain,
                    errorBuilder: (context, error, stackTrace) {
                      return const Icon(Icons.book, size: 100);
                    },
                  )
                      : const Icon(Icons.book, size: 100),
                ),
              ),
              const SizedBox(height: 20),
              Text(
                book['title']?.toString() ?? 'N/A',
                style: const TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 12),
              Text(
                'Author: ${book['author']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.w500,
                  color: Colors.indigo,
                ),
              ),
              const SizedBox(height: 8),
              Text(
                'Price: \$${book['price']?.toString() ?? '0.00'}',
                style: const TextStyle(
                  fontSize: 18,
                  color: Colors.green,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}