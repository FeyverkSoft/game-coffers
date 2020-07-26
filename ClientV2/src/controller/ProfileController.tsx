import * as React from 'react';
import { Breadcrumb, Layout, Col, Row, Statistic } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang, IProfile, ITax } from '../_services';
import { connect } from 'react-redux';
import { IStore } from '../_helpers/store';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { profileInstance } from '../_actions/profile/profile.actions';
import { IHolded } from '../core';
import { ProfileCard } from '../_components/Profile/ProfileCard';
import { Card } from '../_components/Base/Card';
import { TaxCard } from '../_components/Profile/TaxCard';
import { ICharacter } from '../_services/profile/ICharacter';
import { ProfileCharList } from '../_components/Profile/ProfileCharList';
import { ProfileNestList } from '../_components/Nests/ProfileNestList';
import { AddCharDialog } from '../_components/Character/AddCharDialog';
import { Nest } from '../_services/nest/Nest';
import { nestsInstance } from '../_actions/nest/nests.actions';
import { Contract } from '../_services/nest/Contract';
import { AddContractDialog } from '../_components/Nests/AddContractDialog';

interface IProfileProps {
    Get: Function;
    GetTax: Function;
    GetCharacters: Function;
    GetNests: Function;
    GetContracts: Function;
    SetMainChar(charId: string): void;
    DeleteChar(charId: string): void;
    AddChar(id: string, name: string, className: string, isMain: boolean): void;
    profile: IProfile & IHolded;
    tax: ITax & IHolded;
    characters: Array<ICharacter> & IHolded;
    nests: Array<Nest> & IHolded;
    nestContracts: Array<Contract> & IHolded;
    DeleteContract(charId: string): void;
}

interface IState {
    showAddModal: boolean;
    showAddContractModal: boolean;
}

export class _ProfileController extends React.Component<IProfileProps, IState> {
    constructor(props: IProfileProps) {
        super(props);
        this.state = {
            showAddModal: false,
            showAddContractModal: false,
        }
    }

    componentDidMount() {
        if (this.props.profile === undefined)
            this.props.Get();
        if (this.props.tax === undefined || !this.props.tax.userId)
            this.props.GetTax();
        if (this.props.characters === undefined || this.props.characters.length === 0)
            this.props.GetCharacters();
        if (this.props.nests === undefined || this.props.nests.length === 0)
            this.props.GetNests();
        if (this.props.nestContracts === undefined || this.props.nestContracts.length === 0)
            this.props.GetContracts();
    }

    setMainChar = (charId: string) => {
        let { characters } = this.props;
        if (characters.filter(_ => _.id === charId && _.isMain).length === 1)
            return;
        this.props.SetMainChar(charId);
    }

    deleteChar = (charId: string) => {
        this.props.DeleteChar(charId);
    }

    toggleAddCharModal = () => {
        this.setState({ showAddModal: !this.state.showAddModal });
    }

    onAddCharacter = (id: string, name: string, className: string, isMain: boolean) => {
        this.props.AddChar(id, name, className, isMain);
        this.toggleAddCharModal();
    };

    deleteContract = (id: string): void => {
        this.props.DeleteContract(id);
    };

    toggleAddContractModal = () => {
        this.setState({ showAddContractModal: !this.state.showAddContractModal });
    }

    onAddContract = () => { }

    render = () => {
        let { profile, tax, characters, nestContracts } = this.props;
        return <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <Link to={"/"} >
                        <HomeOutlined />
                    </Link>
                </Breadcrumb.Item>
                <Breadcrumb.Item>
                    <Link to={"/profile"} >
                        {Lang("PROFILE_PAGE")}
                    </Link>
                </Breadcrumb.Item>
            </Breadcrumb>
            <Layout>
                <Row gutter={[16, 16]}>
                    <Col xs={24} sm={24} md={14} lg={8} xl={8} xxl={6}>
                        <ProfileCard
                            profile={profile}
                            isLoading={profile.holding !== false}
                        />
                    </Col>
                    <Col xs={24} sm={12} md={5} lg={4} xl={3} xxl={2} >
                        <Card
                            loading={profile.holding}>
                            <Statistic
                                title={Lang('USER_CHAR_COUNT')}
                                valueStyle={{ color: '#3f8600' }}
                                value={profile.charCount}
                                precision={0} />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={5} lg={4} xl={3} xxl={2} >
                        <Card
                            loading={profile.holding}
                        >
                            <Statistic
                                title={Lang('USER_ROW_BALANCE')}
                                value={profile.balance}
                                precision={2}
                                suffix="G"
                            />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={6} lg={4} xl={5} xxl={3} >
                        <Card
                            loading={profile.holding}
                        >
                            <Statistic
                                title={Lang('USER_LOAN_AMOUNT')}
                                value={profile.activeLoanAmount}
                                precision={2}
                                suffix="G"
                            />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={6} lg={4} xl={5} xxl={3} >
                        <Card
                            loading={profile.holding}
                        >
                            <Statistic
                                title={Lang('USER_AMOUNT_PENALTIES')}
                                valueStyle={{ color: '#cf1322' }}
                                value={profile.activePenaltyAmount}
                                precision={2}
                                suffix="G"
                            />
                        </Card>
                    </Col>
                    <Col xs={24} sm={24} md={12} lg={24} xl={24} xxl={8} >
                        <TaxCard
                            loading={tax.holding}
                            tax={tax}
                            charCount={profile.charCount}
                        />
                    </Col>
                </Row>
                <Row gutter={[16, 16]}>
                    <Col xs={24} sm={24} md={24} lg={12} xl={12} >
                        <ProfileCharList
                            characters={characters}
                            loading={characters.holding}
                            SetMainChar={this.setMainChar}
                            DeleteChar={this.deleteChar}
                            AddChar={this.toggleAddCharModal}
                        />
                    </Col>
                    <Col xs={24} sm={24} md={24} lg={12} xl={12} >
                        <ProfileNestList
                            nestContract={nestContracts}
                            loading={nestContracts.holding}
                            DeleteContract={this.deleteContract}
                            AddContract={this.toggleAddContractModal}
                        />
                    </Col>
                </Row>
            </Layout>
            <AddCharDialog
                onClose={this.toggleAddCharModal}
                visible={this.state.showAddModal}
                onAdd={this.onAddCharacter}
                isLoading={characters.holding}
            />
            <AddContractDialog
                onClose={this.toggleAddContractModal}
                visible={this.state.showAddContractModal}
                onAdd={this.onAddContract}
                isLoading={nestContracts.holding}
            />
        </Content>
    }
}

const ProfileController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { userContracts, nests } = state.nests;
        const { profile, tax, characters } = state.profile;
        return { profile, tax, characters, nestContracts: userContracts, nests: nests };
    },
    (dispatch: Function) => {
        return {
            Get: () => dispatch(profileInstance.Get()),
            GetTax: () => dispatch(profileInstance.GetTax()),
            GetCharacters: () => dispatch(profileInstance.GetChars()),
            SetMainChar: (charId: string) => dispatch(profileInstance.SetMainChar(charId)),
            DeleteChar: (charId: string) => dispatch(profileInstance.DeleteChar(charId)),
            AddChar: (id: string, name: string, className: string, isMain: boolean) =>
                dispatch(profileInstance.AddChar({ id, name, className, isMain })),
            GetNests: () => dispatch(nestsInstance.getNestList()),
            GetContracts: () => dispatch(nestsInstance.getMyContracts()),
            DeleteContract: (id: string) => dispatch(nestsInstance.deleteContract({ id })),
        }
    })(_ProfileController);

export default ProfileController;