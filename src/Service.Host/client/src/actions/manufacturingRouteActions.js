import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { manufacturingRouteActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.manufacturingRoute.actionType,
    itemTypes.manufacturingRoute.uri,
    actionTypes,
    config.appRoot
);
