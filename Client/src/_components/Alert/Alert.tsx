import * as React from 'react';
import style from "./alert.module.less";
import { connect, DispatchProp } from 'react-redux';
import { alertInstance } from '../../_actions';
import { IStore } from '../../_helpers';
import { AlertState } from '../../_reducers/alert/alert.reducer';

interface IAlertsProps {
    alerts: AlertState;
}

class Alerts extends React.Component<IAlertsProps & DispatchProp<any>> {
    constructor(props: IAlertsProps & DispatchProp<any>) {
        super(props);
    }

    delete = (x: any) => {
        const { dispatch } = this.props;
        dispatch(alertInstance.delete(x));
    }

    render() {
        const { alerts } = this.props;
        let $this = this;
        return (
            <div className={style["alerts"]}>
                {alerts.messages.map(x => {
                    return (
                        <Alert key={x.id} type={x.type}
                            onClick={() => $this.delete(x.id)}>
                            {x.message}
                        </Alert>
                    )
                })}
            </div>
        );
    }
}

const Alert = ({ ...props }) => {
    return (
        <div className={`${style.alert} ${style[props.type]}`} {...props}>
            {props.children}
        </div>
    )
}

const connectedAlerts = connect<{}, {}, any, IStore>((state: IStore) => {
    const { alerts } = state;
    return {
        alerts
    };
})(Alerts);

export { connectedAlerts as Alerts }; 