import * as React from 'react';
import { LoginForm } from '../_components/LoginForm/LoginForm';
import { Card, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang } from '../_services';
import style from './auth.module.scss';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';

export class AuthController extends React.Component {
    render() {
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
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