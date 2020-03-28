import * as React from 'react';
import { Route, Redirect, RouteProps, RouteComponentProps } from 'react-router-dom';

export const PrivateRoute = ({ component, ...rest }: RouteProps) => {
    if (!component) {
        throw Error("component is undefined");
    }
    let session = localStorage.getItem('session');
    const Component = component;
    const render = (props: RouteComponentProps<any>): (React.ReactNode | Redirect) => {
        return session && session !== null && session !== '' && session !== '{}' ?
            <Component {...rest} {...props} /> :
            <Redirect
                exact={rest.exact}
                strict={rest.strict}
                children={rest.children}
                to={{ pathname: '/auth' }} />
    };

    return (<Route {...rest} render={render} />);
}