import * as React from 'react';
import { LoginForm } from '../_components/LoginForm/LoginForm';
import { Card, Breadcrumb, Tabs, Spin, Result, Button } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang } from '../_services';
import style from './auth.module.scss';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { EmailLoginForm } from '../_components/LoginForm/EmailLoginForm';
import { RegForm } from '../_components/Reg/RegForm';
import { IF, IStore } from '../_helpers';
import { LoadingOutlined } from '@ant-design/icons';
import { connect } from 'react-redux';
import { sessionInstance } from '../_actions/session.actions';
import { history } from '../_helpers';

const antIcon = <LoadingOutlined style={{ fontSize: 64 }} spin />;

interface IProps {
    isFinal: boolean;
    isNew?: boolean;
    location: Location;
    checkCode(code: string): void;
}
interface ISate {
    code: string;
}

class _authController extends React.Component<IProps, ISate> {
    constructor(props: any) {
        super(props);
        this.state = { code: '' };
    }

    componentDidMount() {
        let code: string = '';
        try {
            code = this.props.location.search.replace('?code=', '');
        } catch{
            code = '';
        }
        this.setState({ code: code });
        if (code)
            this.props.checkCode(code);
    }

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
                    <Card>
                        <IF value={this.state.code === '' || this.props.isFinal}>
                            <Tabs defaultActiveKey="1">
                                <Tabs.TabPane tab={Lang("AUTH_PUBLIC")} key={this.props.isNew ? "2" : "1"}>
                                    <EmailLoginForm
                                        guildId={'00000000-0000-4000-0000-000000000003'}
                                    />
                                </Tabs.TabPane>
                                <Tabs.TabPane tab={Lang("REG_PUBLIC")} key={this.props.isNew ? "1" : "2"}>
                                    <RegForm
                                        guildId={'00000000-0000-4000-0000-000000000003'}
                                    />
                                </Tabs.TabPane>
                                <Tabs.TabPane
                                    tab={<span className={style['guild-tab']}>
                                        {Lang("AUTHORIZE_FORM")}
                                    </span>}
                                    key="3"
                                >
                                    <LoginForm />
                                </Tabs.TabPane>
                            </Tabs>

                        </IF>
                        <IF value={this.state.code !== '' && this.props.isFinal === undefined}>
                            <Spin indicator={antIcon} />
                        </IF>
                        <IF value={this.state.code !== '' && this.props.isFinal === false}>
                            <Result
                                status='error'
                                title="Код подтверждения не верен"
                                extra={
                                    <Button onClick={() => history.push('/')}>Назад</Button>
                                }
                            />
                        </IF>
                    </Card>
                </div>
            </Content>
        );
    }
}

const connectedAuthController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        return {
            isFinal: state.session.ConfirmCodeState,
            isNew: state.session.isNew
        };
    },
    (dispatch: any) => {
        return {
            checkCode: (code: string) => dispatch(sessionInstance.checkCode({ code: code }))
        }
    })(_authController);

export default connectedAuthController;