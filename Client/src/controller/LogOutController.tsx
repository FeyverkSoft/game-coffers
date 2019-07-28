import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang } from '../_services';
import { Page, Crumbs, Grid, Col3, СanvasBlock, Form, Col1, Button } from '../_components';

import { sessionInstance } from '../_actions';
import { IStore } from '../_helpers';

interface ILogOutProps extends DispatchProp<any> {
    isLoading?: boolean
}
class LogOut extends React.Component<ILogOutProps> {
    constructor(props: ILogOutProps) {
        super(props);
    }

    logout = () => {
        const { dispatch } = this.props;
        dispatch(sessionInstance.logOut());
    }

    render() {
        return (<Page
            title={Lang("LOGOUT")}
            breadcrumbs={[new Crumbs("./logout", Lang("LOGOUT"))]}
        >
            <Grid
                align="center"
            >
                <Col3>
                    <СanvasBlock
                        type="default"
                        isLoading={this.props.isLoading}
                        title={Lang('logout')}
                    >
                        <Form
                            onSubmit={this.logout}
                            direction="vertical"
                        >
                            <Col1>
                                {<Button
                                    isLoading={this.props.isLoading}
                                    isSubmit={true}
                                    style={{ marginTop: "1rem" }}
                                >{Lang('Logout')}</Button>}
                            </Col1>
                        </Form>
                    </СanvasBlock>
                </Col3>
            </Grid>
        </Page>
        );
    }
}
const connectedLogOut = connect((state: IStore) => {
    const { session } = state;
    return {
        isLoading: session && session.holding
    };
})(LogOut);

export { connectedLogOut as LogOutController }; 