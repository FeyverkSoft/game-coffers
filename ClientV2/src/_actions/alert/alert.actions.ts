import { Alert } from "../../_services";
import { notification } from 'antd';
import { AlertActionsType } from "..";

export class AlertActions {
    /**
     * Запушить сообщение об успехе
     * @param {*} message 
     */
    success(message: string) {
        notification.success({
            message: String(message)
        });
        return AlertActionsType.SUCCESS(new Alert(String(message), 'alert-success'));
    }
    /**
     * Запушить сообщение об ошибке
     * @param {*} message 
     */
    error(message: string): any {
        notification.error({
            message: String(message)
        });
        return AlertActionsType.ERROR(new Alert(String(message), 'alert-error'));
    }
    /**
     * Очистить пуш список
     */
    clear(): any {
        return AlertActionsType.CLEAR();
    }
    /**
     * Запушить информационное сообщение
     * @param {*} message 
     */
    info(message: string): any {
        notification.info({
            message: String(message)
        });
        return AlertActionsType.INFO(new Alert(String(message), 'alert-info'));
    }
    /**
     * Удалить сообщение из пуша по его id
     * @param {*} id id сообщения для удаления
     */
    delete(id: string): any {
        return AlertActionsType.DELETE(id);
    }

}

export const alertInstance = new AlertActions();