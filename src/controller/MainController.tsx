import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang } from '../_services';
import { Crumbs, BaseReactComp, Form, Input, Button, СanvasBlock } from '../_components';

import { sessionInstance } from '../_actions';
import { IStore } from '../_helpers';

interface IMainProps extends DispatchProp<any> {
    isLoading?: boolean;
}

class Main extends BaseReactComp<IMainProps, any> {
    constructor(props: IMainProps) {
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
        return <div className="col-wrapper col-center">
            <СanvasBlock
                type={'important'}
                isLoading={this.props.isLoading}
                title={Lang('Authorize_Form')}
            >
                <Form onSubmit={this.handleSubmit} className='col-wrapper'>
                    <div className='col-1'>
                        <Input
                            label={'UserName'}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='form.username'
                            value={username.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </div>
                    <div className='col-1'>
                        <Input
                            label={'Password'}
                            type='password'
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='form.password'
                            value={password.value}
                            isRequiredMessage={Lang('IsRequired')}
                        /> </div>
                    <div className='col-1 element-padding'>
                        {<Button
                            isLoading={this.props.isLoading}
                            isSubmit={true}
                            disabled={!this.isValidForm(this.state.form)}

                        >{Lang('Login')}</Button>}
                    </div>
                </Form>
            </СanvasBlock>
        </div>
    }
}

const connectedMain = connect<{}, {}, {}, IStore>((state: IStore) => {
    const { session } = state;
    return {
        session,
        isLoading: session && session.holding
    };
})(Main);

export { connectedMain as MainController }; 