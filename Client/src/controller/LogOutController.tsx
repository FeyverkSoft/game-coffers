import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang } from '../_services';
import { Page, Crumbs, Grid, Col3, CanvasBlock, Form, Col1, Button } from '../_components';

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
            title={Lang("LOGOUT_PAGE")}
            breadcrumbs={[new Crumbs("./logout", Lang("LOGOUT_PAGE"))]}
        >
            <Grid
                align="center"
            >
                <Col3>
                    <CanvasBlock
                        type="default"
                        isLoading={this.props.isLoading}
                        title={Lang('LOGOUT_PAGE_')}
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
                    </CanvasBlock>
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