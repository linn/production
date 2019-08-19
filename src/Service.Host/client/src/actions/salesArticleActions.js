import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { salesArticleActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.salesArticle.actionType,
    itemTypes.salesArticle.uri,
    actionTypes,
    config.proxyRoot
);
