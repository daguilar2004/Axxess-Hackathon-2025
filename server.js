// server2.js
const express = require('express');
const fs = require('fs');
const cors = require('cors');
const app = express();
const port = 3000;

app.use(cors());
app.use(express.json());

app.post('/append-chat', (req, res) => {
  const { message } = req.body;
  fs.appendFile('chat_history.txt', '${message}\n', (err) => {
    if (err) {
      console.error(err);
      return res.status(500).send('Error appending to file');
    }
    res.send('Message appended to file');
  });
});

app.get('/download-chat-history', (req, res) => {
  res.setHeader('Content-Disposition', 'attachment; filename=chat_history.txt');
  res.sendFile(__dirname + '/chat_history.txt', (err) => {
    if (err) {
      console.error(err);
      return res.status(500).send('Error downloading file');
    }
  });
});

app.listen(port, () => {
  console.log('Server listening at http://localhost:${port}');
});