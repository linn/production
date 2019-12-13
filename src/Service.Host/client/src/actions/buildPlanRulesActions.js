import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { buildPlanRulesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.buildPlanRules.item,
    itemTypes.buildPlanRules.actionType,
    itemTypes.buildPlanRules.uri,
    actionTypes,
    config.proxyRoot
);
