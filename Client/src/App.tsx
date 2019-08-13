import React, { lazy, Suspense } from "react";
import { Router, Route, Switch, Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { PrivateRoute, Alerts, Space, ProfileButton, Private, Spinner } from './_components';
import { IStore, history, TryCatch } from './_helpers';
import { Logo, Header, HeaderLink } from './_components';
import {
    AuthController,
    LogOutController,
    NotFoundController,
} from './controller';
import { alertInstance } from './_actions';
import { Lang } from './_services';


const load = (Component: any) => (props: any) => (
    <Suspense fallback={<Spinner />}>
        <Component {...props} />
    </Suspense>
);

const BirthdayController = load(lazy(() => import("./controller/BirthdayController")));
const MainController = load(lazy(() => import("./controller/MainController")));
const DemoController = load(lazy(() => import("./controller/DemoController")));

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
                            <HeaderLink to="/" exact>{Lang('COFFERS')}</HeaderLink>
                            <HeaderLink to="/birthday" exact>{Lang('BD')}</HeaderLink>
                        </Private>
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
                                <PrivateRoute path='/birthday' component={BirthdayController} />
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