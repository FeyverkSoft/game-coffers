import * as React from 'react';
import { Route, Redirect, RouteProps, RouteComponentProps } from 'react-router-dom';
import { SessionInfo } from '../_services';

export const PrivateRoute = ({ component, ...rest }: RouteProps) => {
    if (!component) {
        throw Error("component is undefined");
    }
    let session = new SessionInfo(localStorage.getItem('session'));
    const Component = component;
    const render = (props: RouteComponentProps<any>): (React.ReactNode | Redirect) => {
        if (session.roles && session.roles.filter(_ => _.toLowerCase() === 'demo').length > 0 &&
            !(props.location.pathname.toLowerCase().includes('contracts') ||
                props.location.pathname.toLowerCase().includes('profile') ||
                props.location.pathname.toLowerCase().includes('logout')
            ))
            return <Redirect
                exact={rest.exact}
                strict={rest.strict}
                children={rest.children}
                to={{ pathname: '/profile' }} />;

        return session.isActive() ?
            <Component {...rest} {...props} /> :
            <Redirect
                exact={rest.exact}
                strict={rest.strict}
                children={rest.children}
                to={{ pathname: '/auth' }} />
    };

    return (<Route {...rest} render={render} />);
}

export const NotPrivateRoute = ({ component, ...rest }: RouteProps) => {
    if (!component) {
        throw Error("component is undefined");
    }
    let session = new SessionInfo(localStorage.getItem('session'));
    const Component = component;
    const render = (props: RouteComponentProps<any>): (React.ReactNode | Redirect) => {
        return session.isActive() ?
            <Redirect
                exact={rest.exact}
                strict={rest.strict}
                children={rest.children}
                to={{ pathname: '/' }} />
            :
            <Component {...rest} {...props} />
    };

    return (<Route {...rest} render={render} />);
}