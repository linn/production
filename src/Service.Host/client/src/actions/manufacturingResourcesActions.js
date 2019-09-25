import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { manufacturingResourcesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.manufacturingResources.item,
    itemTypes.manufacturingResources.actionType,
    itemTypes.manufacturingResources.uri,
    actionTypes,
    config.appRoot
);
