import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang, GuildInfo, LangF } from '../_services';
import { Crumbs, BaseReactComp, Form, Input, Button, СanvasBlock, Page, MaterialSelect, Grid, Col2, Col1, NamedValue } from '../_components';

import { guildInstance } from '../_actions';
import { IStore } from '../_helpers';

interface GuildInfos {
    charCount: number;
    inFactCharCount: number;
    tax: number;
}
interface IMainProps extends DispatchProp<any> {
    isLoading?: boolean;
    guildId?: string;
    guildInfo: GuildInfo;

}

class Main extends BaseReactComp<IMainProps, any> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
        };
    }

    pageActions = (): React.ReactNode | string => {
        return <div>
            <MaterialSelect></MaterialSelect>
        </div>;
    }

    componentDidMount() {
        if (this.props.guildInfo.id == '' && this.props.guildId)
            this.props.dispatch(guildInstance.GetGuild({ guildId: this.props.guildId }))
    }

    baseInfo = () => {
        return <Grid
            direction="horizontal"
        >
            <Col2>
                <СanvasBlock
                    title={Lang("MAIN_PAGE_MAIN_INFO")}
                    type="important"
                >
                    {/*  <Grid
                        direction="vertical"
                    >
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_CHARACTERS_COUNT")}>
                                {this.props.guildInfo.charCount || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_FACT_CHARACTERS_COUNT")}>
                                {this.props.guildInfo.inFactCharCount || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_TAX")}>
                                {this.props.guildInfo.inFactCharCount || 0}
                            </NamedValue>
                        </Col1>
                    </Grid>
                </СanvasBlock>
            </Col2>
            <Col2>
                <СanvasBlock
                    title={Lang("MAIN_PAGE_ADV_INFO")}
                    type="success"
                >
                    <Grid
                        direction="vertical"
                    >
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_ADV_INFO_BALANCE")}>
                                {this.props.guildInfo.tax || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_ADV_INFO_SALES")}>
                                {this.props.guildInfo.tax || 0}
                            </NamedValue>
                        </Col1>
                        <Col1>
                            <NamedValue name={Lang("MAIN_PAGE_ADV_INFO_SPENT")}>
                                {this.props.guildInfo.tax || 0}
                            </NamedValue>
                        </Col1>
                    </Grid>*/}
                </СanvasBlock>
            </Col2>
        </Grid>;
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
            {this.baseInfo()}
            {this.charactersGrid()}
        </Page>
    }
}

const connectedMain = connect<{}, {}, {}, IStore>((state: IStore) => {
    const { guild } = state.guild;
    return {
        isLoading: guild.holding,
        guildInfo: guild,
        guildId: state.session.guildId
    };
})(Main);

export { connectedMain as MainController }; 