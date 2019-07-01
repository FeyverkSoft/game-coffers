export const formatDateTime = (dateTime: Date, short?: string): string => {
    let date = new Date(dateTime);
    switch (short) {
        case 'd':
            var values: any = [date.getDate(), date.getMonth() + 1, date.getFullYear() % 100];
            for (var id in values) {
                values[id] = values[id].toString().replace(/^([0-9])$/, '0$1');
            }
            return `${values[0]}-${values[1]}-${values[2]}`.replace(' ', ' ')
        case 'h':
            return date.toLocaleTimeString().replace(' ', ' ');
    }
    return date.toLocaleString().replace(' ', ' ');
}