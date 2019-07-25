export const formatDateTime = (dateTime: Date | string, short?: string): string => {
    let date: Date;
    if (`${dateTime}`.length < 10)
        return `${dateTime}`;
    try {
        date = new Date(dateTime);
    } catch (e) {
        console.debug(e);
        return `${dateTime}`;
    }
    switch (short) {
        case 'd':
            var values: any = [date.getFullYear(), date.getMonth() + 1, date.getDate()];
            for (var id in values) {
                values[id] = values[id].toString().replace(/^([0-9])$/, '0$1');
            }
            return `${values[0]}-${values[1]}-${values[2]}`.replace(' ', ' ')
        case 'h':
            return date.toLocaleTimeString().replace(' ', ' ');
    }
    return date.toLocaleString().replace(' ', ' ');
}