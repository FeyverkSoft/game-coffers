import memoize from "lodash.memoize";
const locString: any = {
    en: {
        NOT_FOUND: 'Resource {0} not found;',
        NOT_IMPLEMENTED: 'Action or function {0} not implemented',
        INTERNAL_SERVER_ERROR: 'Internal server error {0}',
        FORBIDDEN: 'Unauthorized. {0}',
        INCORRECT_PASSWORD: 'incorrect username or password. {0}',
        POINT_NOT_FOUND: 'Point {0} not found',
        INVALID_ARGUMENT: 'invalid argument {0}',
        RESOURCE_NOT_FOUND: 'Resource {0} not found;'
    },
    ru: {
        NOT_FOUND: 'Ресурс {0} не найден;',
        NOT_IMPLEMENTED: 'Метод или функционал {0} не реализован',
        INTERNAL_SERVER_ERROR: 'Произошла внутреняя ошибка сервера: {0}',
        FORBIDDEN: 'Доступ запрещён. {0}',
        INCORRECT_PASSWORD: 'Введён не верный логин и/или пароль. {0}',
        POINT_NOT_FOUND: 'Точка {0} не найдена',
        INVALID_ARGUMENT: 'Передан недопустимый аргумент {0}',
        RESOURCE_NOT_FOUND: 'Ресурс {0} не найден;',
        MAIN_PAGE: 'Казна гильдии: {0}',
        COFFERS: 'Казна',
        BD: 'ДР',
        REVERSE: '!СТОРНО!',
        MAIN_PAGE_MAIN_INFO: 'Сводка',
        MAIN_PAGE_MAIN_BALANCE: 'Баланс',
        MAIN_PAGE_CHARACTERS_COUNT: 'Чаров в гильдии:',
        MAIN_PAGE_GAMERS_COUNT: 'Игроков в гильдии:',
        MAIN_PAGE_ADV_INFO: 'Баланс за период',
        MAIN_PAGE_ADV_INFO_BALANCE: "Баланс ги на данный момент:",
        MAIN_PAGE_ADV_INFO_SALES: "Продано со склада гильдии:",
        MAIN_PAGE_ADV_INFO_SPENT: "Потрачено на нужды гильдии:",
        MAIN_PAGE_CHARACTERS_GRID: "Список персонажей",
        MAIN_PAGE_GUILD_BALANCE: 'Баланс ги',
        MAIN_PAGE_GUILD_LOANS: 'Погашено от займов: ',
        NEW_CHAR_MODAL: 'Добавление нового чара',
        NAME: "Имя",
        LOGIN: 'Логин',
        LOGOUT: 'Выход',
        LOGOUT_PAGE: 'Страница выхода',
        LOGOUT_PAGE_: 'Подтвердите выход',
        DATEOFBIRTH: 'День рождения',
        CLASS_NAME: 'Класс',
        MAIN_PAGE_EXPECTED_TAX: 'Налоги: ',
        MAIN_PAGE_EXPECTED_TAX_FORMAT: "{0} из {1}",
        MAIN_PAGE_GUILD_LOANS_FORMAT: "{1} из {0}",
        MAIN_PAGE_GUILD_B_F: '{0} / {1} / {2}',
        TARIFF_TITLE: 'Тариф',
        TARIFF_TAX: 'Налог за 1 чара:',
        TARIFF_LOAN_TAX: 'Стоимость займа в день',
        TARIFF_LOAN_EXPTAX: 'Стоимость просрочки займа',
        USER_CHAR_LIST: 'персонажи игрока: {0}',
        USER_CHAR_COUNT: 'Персонажей',
        USER_ROW_STATUS: 'Статус',
        USER_ROW_RANK: 'Ранг',
        OPERATION: 'Операция',
        USER_ROW_BALANCE: 'Баланс',
        PROFILE: 'Профиль',
        OPERATIONS: 'Операции',
        USER_ROW_PENALTIES: 'Штрафы',
        USER_ROW_LOANS: 'Займы',
        NEW_USER_MODAL: 'Новый пользователь',
        USER_ROLE: {
            Leader: 'ГМ',
            Officer: 'Офицер',
            Veteran: 'Ветеран',
            Soldier: 'Солдат',
            Beginner: 'Новобранец',
        },
        USER_STATUS: {
            Afk: 'Афк',
            Banned: 'Забанен',
            Active: 'Активный',
            Left: 'Ушёл',
            New: 'Новичок',
        },
        LOAN_STATUS: {
            Active: 'Активный',
            Paid: 'Выплачен',
            Expired: 'Стух',
            Canceled: 'Отменён'
        },
        PENALTY_STATUS: {
            Active: 'Активный',
            InActive: 'Уплачен',
            Canceled: 'Отменён'
        },
        MAIN_RECRUITMENTSTATUS: 'Набор:',
        ADD_NEW_USER: '+',
        PAGE_AUTH: "Авторизация",
        AUTHORIZE_FORM: "Введите логин и пароль",
        USER_PENALTY_AMOUNT: "ШТРАФ: {0}",
        USER_BALANCE: 'Ваш баланс',
        USER_TITLE: "Вы",
        USER_TAX_AMOUNT: 'Сумма налога',
        USER_LOAN_AMOUNT: 'Сумма активных займов',
        NEW_LOAN_MODAL: 'Новый займ',
        NEW_PENALTY_MODAL: 'Новый штраф',
        SHOW_PENALTY_MODAL: 'Информация по штрафу',
        SHOW_LOAN_MODAL: 'Информация по займу',
        RECRUITMENTSTATUS: {
            Open: 'Открыт',
            Close: 'Закрыт',
            Internal: 'По приглашению'
        },
        NEW_OPERATION_MODAL: 'Новая операция',
        MODAL_LOAN_AMOUNT: 'Сумма',
        MODAL_LOAN_BALANCE: 'Осталось',
        MODAL_LOAN_DATE: 'Дата',
        MODAL_LOAN_EXPIREDDATE: 'Дата истечения',
        MODAL_LOAN_DESCRIPTION: 'Описание',
        MODAL_LOAN_STATUS: 'Статус',
        MODAL_PENALTY_AMOUNT: 'Сумма',
        MODAL_PENALTY_DATE: 'Дата',
        MODAL_PENALTY_STATUS: 'Статус',
        MODAL_PENALTY_DESCRIPTION: 'Причина',
        ADD: 'Добавить',
        PENALTY_AMOUNT: 'Сумма штрафа',
        PENALTY_DESCRIPTION: 'Описание',
        LOAN_AMOUNT: 'Сумма займа',
        LOAN_BORROWDATE: 'Дата',
        LOAN_EXPIREDDATE: 'Дата стухания',
        LOAN_DESCRIPTION: 'Описание',
        MODAL__OPERATIONS: 'Операции',
        BIRTHDAY_PAGE: 'Дни рождения',
        BIRTHDAY_TILE: 'Участники',
        OPERATIONS_TYPE: {
            Tax: 'Погашение налога',
            Sell: 'Операция продажи предмета со склада',
            Penalty: 'Погашение штрафа',
            Loan: 'Погашение займа',
            Exchange: 'Обмен между твинами',
            Output: 'Вывод во внешнюю систему',
            InternalOutput: 'Вывод на счёт игрока',
            Emission: 'Эмиссия',
            InternalEmission: 'Пополнение со счёта игрока',
            Other: 'Иное',
        },
        OPERATION_AMOUNT: 'Сумма операции',
        OPERATION_DESCRIPTION: 'Описание операции',
        OPERATION_FROMUSERID: 'С пользователя:',
        OPERATION_TOUSERID: 'Пользователю:',
        OPERATION_PENALTY: 'Уплата штрафа',
        OPERATION_LOANS: 'Уплата займа',
        USER_AMOUNT_PENALTIES: 'Непогашеных штрафов на',
        USER_SALE: 'Уровень налога за чара',
        CHAR_IS_MAIN: 'Основа',
        PROFILE_CHARACTERS: 'Ваши персонажи',
        CHAR_ACTIONS: 'Действия',
        NEW_CHAR: 'Новый персонаж'
    }
};

let _CurrentLang: string | undefined;
export const getCookie = function getCookie(name: string): string | undefined {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

export const setCookie = function setCookie(name: string, val: string): void {
    document.cookie = `${name}=${val}; path=../`;
}

export const CurrentLang = function (): string {
    if (_CurrentLang)
        return _CurrentLang;
    _CurrentLang = getCookie("Lang") || 'ru';
    return _CurrentLang;
}

function getTranslate(value: string): string {
    if (!value)
        return value;
    var lang = CurrentLang();
    var locStr: any = {};
    if (locString[lang])
        locStr = locString[lang];
    else {
        locStr = locString["ru"];
        setCookie("Lang", "ru");
    }

    let lowerVal = value.replace(/ /gi, '').toLowerCase();

    let loc = locStr[value] || locStr[lowerVal];
    if (loc)
        return loc;

    for (let key in locStr) {
        if (key.toLowerCase() == lowerVal)
            return locStr[key];
    }
    return locString["ru"][lowerVal] || locString["ru"][value];
}


export const DLang = memoize((value: string, key?: string): string => {
    if (!key)
        return '';
    let res: any;
    if (value)
        res = getTranslate(value.toUpperCase()) || value;
    else
        res = getTranslate(CurrentLang()) || value;
    return res[key] || '';
}, (it, ...arg) => {
    return it + arg;
})

export const Lang = memoize((value: string, count?: number | undefined): string => {
    let res: any;
    if (value)
        res = getTranslate(value.toUpperCase()) || value;
    else
        res = getTranslate(CurrentLang()) || value;

    if (count && res instanceof Object) {
        let keys = Object.keys(res).sort();
        if (res[count])
            return res[count].replace(/\{0\}/ig, count);

        for (let i = 0; i < keys.length; i++) {
            if (keys[i] > count.toString())
                return res[keys[i - 1]].replace(/\{0\}/ig, count);
        }
        return res[keys[0]].replace(/\{0\}/ig, count);
    }

    if (count)
        return res.replace(/\{0\}/ig, count);
    return res;
}, (it, ...arg) => {
    return it + arg;
})

export const LangF = memoize((value: string, ...arg: (string | number)[]) => {
    let l = Lang(value);
    console.debug(l);
    Object.keys(arg).forEach((element, i) => {
        l = l.replace('{' + i + '}', String(arg[i]));
    })
    return l;
}, (it, ...arg) => {
    return it + arg;
})