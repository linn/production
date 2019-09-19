import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { worksOrdersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

// TODO should this be fetch api actions?
export default new UpdateApiActions(
    itemTypes.worksOrders.actionType,
    itemTypes.worksOrders.uri,
    actionTypes,
    config.appRoot
);
