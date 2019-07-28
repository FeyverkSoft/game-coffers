import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang } from '../_services';
import { Crumbs, BaseReactComp, Form, Input, Button, СanvasBlock, Col1, Page, Grid, Col3 } from '../_components';

import { sessionInstance } from '../_actions';
import { IStore } from '../_helpers';

interface IAuthProps extends DispatchProp<any> {
    isLoading?: boolean;
}

class Auth extends BaseReactComp<IAuthProps, any> {
    constructor(props: IAuthProps) {
        super(props);
        this.state = {
            form: {
                username: {},
                password: {}
            }
        };
    }

    handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const { username, password } = this.state.form;
        const { dispatch } = this.props;
        if (this.isValidForm(this.state.form)) {
            dispatch(sessionInstance.logIn(username.value, password.value));
        }
    }

    render() {
        const { username, password } = this.state.form;
        return <Page
            title={Lang("PAGE_AUTH")}
            breadcrumbs={[new Crumbs("./auth", Lang("PAGE_AUTH"))]}
        >
            <Grid
                align="center"
            >
                <Col3>
                    <СanvasBlock
                        type="default"
                        isLoading={this.props.isLoading}
                        title={Lang('Authorize_Form')}
                    >
                        <Form
                            onSubmit={this.handleSubmit}
                            direction="vertical"
                        >
                            <Col1>
                                <Input
                                    label={'UserName'}
                                    onChange={this.onInputVal}
                                    isRequired={true}
                                    path='form.username'
                                    value={username.value}
                                    isRequiredMessage={Lang('IsRequired')}
                                />
                            </Col1>
                            <Col1>
                                <Input
                                    label={'Password'}
                                    type='password'
                                    onChange={this.onInputVal}
                                    isRequired={true}
                                    path='form.password'
                                    customValidator={(str) => `${str}`.length >= 8}
                                    value={password.value}
                                    isRequiredMessage={Lang('IsRequired')}
                                />
                            </Col1>
                            <Col1>
                                {<Button
                                    isLoading={this.props.isLoading}
                                    isSubmit={true}
                                    disabled={!this.isValidForm(this.state.form)}
                                    style={{ marginTop: "1rem" }}
                                >{Lang('Login')}</Button>}
                            </Col1>
                        </Form>
                    </СanvasBlock>
                </Col3>
            </Grid>
        </Page>
    }
}

const connectedAuth = connect<{}, {}, {}, IStore>((state: IStore) => {
    const { session } = state;
    return {
        session,
        isLoading: session && session.holding
    };
})(Auth);

export { connectedAuth as AuthController }; 