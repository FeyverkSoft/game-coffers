import * as React from 'react';
import { LoginForm } from '../_components/LoginForm/LoginForm';
import { Card, Breadcrumb, Icon } from 'antd';
import { Lang } from '../_services';
import style from './auth.module.less';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';

export class AuthController extends React.Component {
    render() {
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <Icon type="home" />
                        </Link>
                    </Breadcrumb.Item>
                    <Breadcrumb.Item>
                        <Link to={"/auth"} >
                            {Lang("PAGE_AUTH")}
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['auth']}>
                    <Card
                        title={Lang("AUTHORIZE_FORM")}
                        style={{ width: '500px' }}
                    >
                        <LoginForm />
                    </Card>
                </div>
            </Content>
        );
    }
}