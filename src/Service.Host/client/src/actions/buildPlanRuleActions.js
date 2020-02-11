import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { buildPlanRuleActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.buildPlanRule.item,
    itemTypes.buildPlanRule.actionType,
    itemTypes.buildPlanRule.uri,
    actionTypes,
    config.proxyRoot
);
