const express = require("express");
const fs = require("fs");
const cors = require("cors");

const app = express();
app.use(express.json());
app.use(cors());

app.post("/save-message", (req, res) => {
  const message = req.body.message;
  fs.appendFile("chat-log.txt", message + "\n", (err) => {
    if (err) {
      res.status(500).send("Error saving message.");
    } else {
      res.send("Message saved.");
    }
  });
});

app.listen(3000, () => console.log("Server running on port 3000"));
