import * as React from 'react';
import { LoginForm } from '../_components/LoginForm/LoginForm';
import { Card } from 'antd';
import { Lang } from '../_services';
import style from './auth.module.less';

export class AuthController extends React.Component {
    render() {
        return (
            <div className={style['auth']}>
                <Card
                    title={Lang("AUTHORIZE_FORM")}
                    style={{ width: '500px' }}
                >
                    <LoginForm />
                </Card>
            </div>
        );
    }
}