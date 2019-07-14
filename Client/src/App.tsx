import * as React from 'react';
import { Router, Route, Switch, Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { PrivateNavLink, PrivateRoute, Alerts, OnlyPublicNavLink, Space, ProfileButton, Private } from './_components';
import { IStore, history, TryCatch } from './_helpers';
import { Logo, Header } from './_components';
import {
    DemoController,
    AuthController,
    LogOutController,
    MainController,
    NotFoundController,
} from './controller';
import { alertInstance } from './_actions';


class MyApp extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        history.listen((location, action) => {
            const { dispatch, alerts } = this.props;
            // очищаем сообщения при изменении локации
            if (alerts && alerts.messages && alerts.messages.length != 0)
                dispatch(alertInstance.clear());
        });
    }

    render() {
        return (
            <Router
                history={history} >
                <div className="flex-vertical">
                    <Header>
                        <Link to="./">
                            <Logo />
                        </Link>
                        <Space />
                        <Private>
                            <ProfileButton />
                        </Private>
                    </Header>
                    <div className="body">
                        <TryCatch>
                            <Switch>
                                <PrivateRoute path='/demo' component={DemoController} />
                                <Route path='/auth' component={AuthController} />
                                <PrivateRoute path='/logout' component={LogOutController} />
                                <PrivateRoute path="/" component={MainController} />
                                <Route component={NotFoundController} />
                            </Switch>
                        </TryCatch>
                    </div>
                    <Alerts />
                </div>
            </Router>
        )
    }
}

const connectedMyApp = connect((state: IStore) => {
    const { session, alerts } = state;
    return {
        session,
        alerts
    };
})(MyApp);
export { connectedMyApp as MyApp }; 