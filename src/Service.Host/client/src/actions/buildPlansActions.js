import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { buildPlansActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.buildPlans.item,
    itemTypes.buildPlans.actionType,
    itemTypes.buildPlans.uri,
    actionTypes,
    config.appRoot
);
