import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang, GuildInfo, LangF, ITariffs, DLang, IGamerInfo, GamerRank } from '../_services';
import {
    Crumbs, BaseReactComp, Form, Input, Button, СanvasBlock, Page, MaterialSelect, Grid,
    Col2, Col1, NamedValue, TariffView, UserView
} from '../_components';

import { guildInstance } from '../_actions';
import { IStore } from '../_helpers';

interface IMainProps extends DispatchProp<any> {
    isLoading?: boolean;
    userLoading?: boolean;
    user: IGamerInfo;
    tariffs: ITariffs;
    guildId?: string;
    guildInfo: GuildInfo;
    userRank: GamerRank;
}

class Main extends BaseReactComp<IMainProps, any> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
        };
    }

    pageActions = (): React.ReactNode | string => {
        return <div>
        </div>;
    }

    componentDidMount() {
        if (this.props.guildInfo.id == '' && this.props.guildId)
            this.props.dispatch(guildInstance.GetGuild({ guildId: this.props.guildId }))
    }

    baseInfo = () => {
        const { guildInfo } = this.props;
        return (
            <СanvasBlock
                title={Lang("MAIN_PAGE_MAIN_INFO")}
                type="important"
                isLoading={this.props.isLoading}
            >
                <Grid
                    direction="horizontal"
                >
                    <Col2>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_CHARACTERS_COUNT")}>
                                {guildInfo.charactersCount || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_GAMERS_COUNT")}>
                                {guildInfo.gamersCount || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_RECRUITMENTSTATUS")}>
                                {DLang('RECRUITMENTSTATUS', guildInfo.recruitmentStatus)}
                            </NamedValue>
                        </Col1>
                    </Col2>
                    <Col2>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_GUILD_BALANCE")}>
                                {guildInfo.charactersCount || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_GUILD_LOANS")}>
                                {guildInfo.gamersCount || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_EXPECTED_TAX")}>
                                {0}
                            </NamedValue>
                        </Col1>
                    </Col2>
                </Grid>
            </СanvasBlock>
        );
    }

    charactersGrid = () => {
        return <СanvasBlock
            title={Lang("MAIN_PAGE_CHARACTERS_GRID")}
            type="success"
        >
        </СanvasBlock>;
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
                <Col2> {this.baseInfo()} </Col2>
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
            {this.charactersGrid()}
        </Page>
    }
}

const connectedMain = connect<{}, {}, {}, IStore>((state: IStore) => {
    const { guild, tariffs } = state.guild;
    const { currentGamer } = state.gamers;
    return {
        isLoading: guild.holding,
        guildInfo: guild,
        userRank: currentGamer.rank,
        user: currentGamer,
        tariffs: tariffs,
        guildId: state.session.guildId
    };
})(Main);

export { connectedMain as MainController }; 