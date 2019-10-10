function priorityText(priority) {
    switch (priority) {
        case '1':
            return 'This part is needed to satisfy back orders.';
        case '2':
            return 'This part is needed by internal customers to reach their trigger levels.';
        case '3':
            return 'You need some of this to reach your trigger level.';
        case '4':
            return 'Everything is fine. Chill out.';
        case '5':
            return 'You have too much stock.';
        default:
            return `Unknown priority level ${priority}`;
    }
}

export default priorityText;
