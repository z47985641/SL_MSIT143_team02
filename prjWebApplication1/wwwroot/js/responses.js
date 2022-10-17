function getBotResponse(input) {
    //rock paper scissors
    if (input == "rock") {
        return "paper";
    } else if (input == "paper") {
        return "scissors";
    } else if (input == "scissors") {
        return "rock";
    }

    // Simple responses
    if (input == "哈囉") {
        return "Hello 很高興為您服務,請問有什麼可以幫助您!";
    } else if (input == "掰掰") {
        return "謝謝您，再見!";
    } else {
        return "請您詳細說明，謝謝!";
    }
}