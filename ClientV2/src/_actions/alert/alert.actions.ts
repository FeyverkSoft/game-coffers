import { AlertActionsType } from "..";
import { Alert } from "../../_services";
import { notification } from 'antd';

export class AlertActions {
    /**
     * Запушить сообщение об успехе
     * @param {*} message 
     */
    success(message: string) {
        notification.success({
            message: String(message)
        });
        return { type: AlertActionsType.SUCCESS, alert: new Alert(String(message), 'alert-success') };
    }
    /**
     * Запушить сообщение об ошибке
     * @param {*} message 
     */
    error(message: string): any {
        notification.error({
            message: String(message)
        });
        return { type: AlertActionsType.ERROR, alert: new Alert(String(message), 'alert-error') };
    }
    /**
     * Очистить пуш список
     */
    clear(): any {
        return { type: AlertActionsType.CLEAR };
    }
    /**
     * Запушить информационное сообщение
     * @param {*} message 
     */
    info(message: string): any {
        notification.info({
            message: String(message)
        });
        return { type: AlertActionsType.INFO, alert: new Alert(String(message), 'alert-info') };
    }
    /**
     * Удалить сообщение из пуша по его id
     * @param {*} id id сообщения для удаления
     */
    delete(id: string): any {
        return { type: AlertActionsType.DELETE, id };
    }

}

export const alertInstance = new AlertActions();