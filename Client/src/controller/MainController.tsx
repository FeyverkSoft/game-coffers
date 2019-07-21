import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang, GuildInfo, LangF, ITariffs, IGamerInfo, GamerRank, GuildBalanceReport, IGamersListView, IGuild, GamerStatus } from '../_services';
import {
    Crumbs, BaseReactComp, СanvasBlock, Page, Grid,
    Col2, TariffView, UserView, MainView, BalanceView, GamerRowView, Dialog, Button, Form, Col1, Input, Private
} from '../_components';

import { guildInstance, gamerInstance } from '../_actions';
import { IStore } from '../_helpers';
import { IHolded } from '../core';
import { AddUserDialog } from '../_components/Dialogs/AddUserDialog';
import { AddCharDialog } from '../_components/Dialogs/AddCharDialog';

interface IMainProps {
    isLoading?: boolean;
    userLoading?: boolean;
    user: IGamerInfo;
    tariffs: ITariffs;
    guildId?: string;
    guildInfo: GuildInfo;
    gamers: Array<IGamersListView>;
    balance: GuildBalanceReport & IHolded;
    userRank: GamerRank;
}

class Main extends BaseReactComp<IMainProps & DispatchProp<any>, any> {
    constructor(props: IMainProps & DispatchProp<any>) {
        super(props);
        this.state = {
            addNewUser: {
                isDisplayed: false,
            },
            addChar: {
                isDisplayed: false,
                userId: ''
            }
        };
    }

    pageActions = (): React.ReactNode | string => {
        return <div>
        </div>;
    }

    componentDidMount() {
        if (this.props.guildInfo.id == '' && this.props.guildId)
            this.props.dispatch(guildInstance.GetGuild({ guildId: this.props.guildId }))
        if (this.props.guildId)
            this.props.dispatch(guildInstance.GetGuildBalanceReport({ guildId: this.props.guildId }))
        if (this.props.guildId)
            this.props.dispatch(gamerInstance.GetGamers({ guildId: this.props.guildId }))
    }

    onAddChar = (userId: string) => {
        this.setState({ addChar: { isDisplayed: true, userId: userId } })
    }

    onDeleteChar = (userId: string, char: string) => {
        if (userId)
            this.props.dispatch(gamerInstance.DeleteCharacters({
                gamerId: userId,
                name: char
            }));
    }

    onSetRank = (userId: string, rank: GamerRank) => {
        if (userId)
            this.props.dispatch(gamerInstance.SetRank({
                gamerId: userId,
                rank: rank
            }));
    }

    onSetStatus = (userId: string, status: GamerStatus) => {
        if (userId)
            this.props.dispatch(gamerInstance.SetStatus({
                gamerId: userId,
                status: status
            }));
    }

    addUserRenderer = () => {
        return (<AddUserDialog
            isDisplayed={this.state.addNewUser.isDisplayed}
            onClose={() => this.setState({ addNewUser: { false: false } })}
            guildId={this.props.guildId || ''}
        ></AddUserDialog>)
    }

    charactersGrid = () => {
        const { gamers } = this.props;
        return <СanvasBlock
            title={Lang("MAIN_PAGE_CHARACTERS_GRID")}
            type="success"
            subType='none'
            subChildren={<Private roles={['admin', 'leader', 'officer']}>
                <Button
                    type={'default'}
                    onClick={() => this.setState({ addNewUser: { isDisplayed: true } })}
                    isSmall={true}
                >{Lang('ADD_NEW_USER')}</Button>
            </Private>}
        >
            {
                gamers.map(g => {
                    return <GamerRowView
                        key={g.id}
                        gamer={g}
                        onAddChar={this.onAddChar}
                        onDeleteChar={this.onDeleteChar}
                        onRankChange={this.onSetRank}
                        onStatusChange={this.onSetStatus}
                    />;
                })
            }
        </ СanvasBlock>;
    }

    render() {
        return <Page
            title={LangF("MAIN_PAGE", this.props.guildInfo.name)}
            breadcrumbs={[new Crumbs("./", LangF("MAIN_PAGE", this.props.guildInfo.name))]}
            pageActions={this.pageActions()}
        >
            <Grid
                direction="horizontal"
            >
                <Col2>
                    <MainView
                        guildInfo={this.props.guildInfo}
                        isLoading={this.props.isLoading}
                    />
                </Col2>
                <Col2>
                    <BalanceView
                        balance={this.props.balance}
                    />
                </Col2>
                <Col2>
                    <TariffView
                        tariff={this.props.tariffs}
                        currentRole={this.props.userRank}
                        isLoading={this.props.isLoading}
                    />
                </Col2>
                <Col2>
                    <UserView
                        user={this.props.user}
                        isLoading={this.props.isLoading}
                        tax={this.props.tariffs[this.props.userRank].tax}
                    />
                </Col2>
            </Grid>
            <Private roles={['admin', 'leader', 'officer']}>
                {this.addUserRenderer()}
            </Private>
            {this.charactersGrid()}
            <AddCharDialog
                userId={this.state.addChar.userId}
                isDisplayed={this.state.addChar.isDisplayed}
                onClose={() => this.setState({ addChar: { isDisplayed: false } })}
            ></AddCharDialog>
        </Page>
    }
}

const connectedMain = connect<{}, {}, {}, IStore>((state: IStore): IMainProps => {
    const { guild, tariffs, reports } = state.guild;
    const { currentGamer, gamersList } = state.gamers;
    return {
        isLoading: guild.holding,
        guildInfo: guild as GuildInfo,
        userRank: currentGamer.rank,
        user: currentGamer,
        tariffs: tariffs,
        guildId: state.session.guildId,
        balance: reports.balanceReport,
        gamers: Object.keys(gamersList).map(k => gamersList[k])
    };
})(Main);

export { connectedMain as MainController }; 